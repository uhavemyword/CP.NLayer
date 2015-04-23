$(document).ready(function () {
    // 字符验证
    jQuery.validator.addMethod("username", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9_]+$/.test(value);
    });

    jQuery.extend(jQuery.validator.defaults, { errorElement: "span" });

    $('form').validate();
});