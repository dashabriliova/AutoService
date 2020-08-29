// После загрузки страницы
$(document).ready(function () {
    //найти все элементы класса page-link
    //и подписаться на событие click
    $(".page-link").click(function (e) {
        // запретить обработку события по умолчанию
        e.preventDefault();
        // получить адрес из текущего элемента
        var url = $(this).attr("href");
        // найти контейнер по id
        // и загрузить в него разметку из полученного адреса
        $("#list").load(url);
        // снять выделение предыдущей страницы пейджера
        $(".active").removeClass("active");
        // выделить текущую страницу пейджера
        var p = $(this).parent().addClass("active");
        // изменить адресную строку браузера
        history.pushState(null, null, url);
    });
});