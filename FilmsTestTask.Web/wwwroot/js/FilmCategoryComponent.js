$(document).ready(function () {
    var categories = [];
    var categoryCount = 0; // Відстежуємо кількість доданих категорій для індексації

    // Функція завантаження категорій і ініціалізація вже вибраних
    function loadCategories() {
        $.get('https://localhost:44360/api/Category', function (data) {
            categories = data;
            var options = categories.map(c => `<option value="${c.id}">${c.name}</option>`);
            $('#categoriesSelect').html(options.join(''));
            initializeSelectedCategories(); // Ініціалізуємо вже вибрані категорії, якщо такі є
        });
    }

    // Ініціалізація вже вибраних категорій
    function initializeSelectedCategories() {
        $('#selectedCategories li').each(function () {
            var categoryId = $(this).data('id');
            var categoryName = $(this).text().trim().replace("Видалити", ""); // Видаляємо текст кнопки з назви категорії
            categoryCount++;
        });

        $('#categoriesSelect').select2({
            width: '100%',
            placeholder: "Оберіть категорію",
            allowClear: true,
            minimumResultsForSearch: Infinity
        });
    }

    function addCategoryToList(id, name) {
        if (!$('#selectedCategories li').filter(function () { return $(this).data('id') == id; }).length) {
            var listItem = `
                <li class="row selector-item" data-id="${id}">
                    <div class="col-8">
                        ${name}
                    </div>
                    <button type="button" class="col-4 btn btn-outline-danger remove-category">X</button>
                </li>`;
            var hiddenInput = `<input type="hidden" name="FilmCategories[${categoryCount}].CategoryId" value="${id}" />`;
            $('#selectedCategories').append(listItem);
            $('#selectedCategories').children().last().append(hiddenInput);
            categoryCount++; // Збільшуємо індекс для наступного елементу
        } else {
            alert('Ця категорія вже додана.');
        }
    }


    $('#showCategories').click(function () {
        $('#categorySelector').show();
    });

    $('#addCategory').click(function () {
        var selectedCategoryId = $('#categoriesSelect').val();
        var selectedCategoryName = $('#categoriesSelect option:selected').text();
        addCategoryToList(selectedCategoryId, selectedCategoryName);
    });

    $('#selectedCategories').on('click', '.remove-category', function () {
        $(this).parent().remove();
        // Оновлюємо індексацію прихованих полів
        $('#selectedCategories li').each(function (index) {
            $(this).find('input').attr('name', `FilmCategories[${index}].CategoryId`);
        });
        categoryCount--; // Зменшуємо лічильник індексів
    });

    loadCategories();
});
