$(document).ready(function () {
    function isChildOf(categoryId, parentId, categories) {
        if (categoryId === Number(parentId)) {
            return true;
        }
        const category = categories.find(c => c.id === categoryId);
        if (category && category.parentCategoryId) {
            return isChildOf(category.parentCategoryId, parentId, categories);
        }
        return false;
    }

    var currentCategoryId = $('#currentCategoryId').val();
    var currentParentId = $('#ParentCategoryId').val();

    $.ajax({
        url: 'https://localhost:44360/api/Category',
        type: 'GET',
        dataType: 'json',
        success: function (categories) {

            // Фільтрація категорій, які не є дочірніми
            const filteredCategories = categories.filter(category =>
                !isChildOf(category.id, currentCategoryId, categories)
            );



            // Сортування за іменем
            filteredCategories.sort(function (a, b) {
                return a.name.localeCompare(b.name);
            });

            // Очищення існуючих опцій в select елементі
            const selectElement = $('.parent-category-selector');
            selectElement.empty();

            // Додавання 'placeholder'
            selectElement.append($('<option>', {
                value: '',
                text: 'Оберіть категорію'
            }));

            // Додавання отриманих опцій
            filteredCategories.forEach(function (category) {
                selectElement.append($('<option>', {
                    value: category.id,
                    text: category.name,
                    selected: category.id === Number(currentParentId)
                }));
            });

            // Ініціалізація Select2
            selectElement.select2({
                width: '100%',
                placeholder: "Оберіть категорію",
                allowClear: true,
                minimumResultsForSearch: Infinity
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Обробка помилок запиту
            console.log('Error fetching categories:', textStatus, errorThrown);
        }
    });
});
