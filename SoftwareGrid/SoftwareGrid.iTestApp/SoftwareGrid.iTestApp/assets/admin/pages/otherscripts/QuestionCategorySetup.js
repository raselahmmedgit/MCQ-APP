﻿var frmId = "frmQuestionCategory";
var QuestionCategorySetup = function () {

    var validateForm = function () {
        if ($().validate) {
            var form = $("#" + frmId);
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true,
                errorElement: 'span',
                errorClass: 'help-block help-block-error',
                focusInvalid: false,

                rules: {
                    QuestionCategoryName: {
                        required: true,
                        maxlength: 150
                    },
                },
                errorPlacement: function (error, element) {
                    var errorContainer = element.parents('div.form-group div.col-md-9');
                    errorContainer.append(error);
                },
                messages: {
                    QuestionCategoryName: {
                        required: "Question Category Name is required."
                    },
                },
                invalidHandler: function (event, validator) {
                    success.hide();
                    error.show();
                },
                highlight: function (element) {
                    $(element)
                        .closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element)
                        .closest('.form-group').removeClass('has-error');
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error');
                },
                submitHandler: function (form) {
                    if ($('#BtnSave').length > 0) {
                        var url = "/Admin/CreateQuestionCategory";
                        $.post(url, $(form).serializeArray(),
                            function (res) {
                                if (res.MessageType == "2") {
                                    resetForm();
                                    loadData();
                                }
                                RB.notifier(res.CurrentMessage, res.MessageType);
                            });
                    } else {
                        form.submit(function (e) { });
                    }

                }
            });
        }
    };

    var actionHandler = function () {

        $(document).on("click", "#BtnCancel", function () {
            resetForm();
        });

        $(document).on("click", ".edit", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                RB.sendAjaxRequest('/Admin/GetQuestionCategory', { questionCategoryId: id }, true, function (res) {
                    if (res != null) {
                        $("#QuestionCategoryId").val(res.QuestionCategoryId);
                        $("#QuestionCategoryName").val(res.QuestionCategoryName);
                        $("#QuestionCategoryIcon").val(res.QuestionCategoryIcon);
                    }
                }, true, true, null);
            }
        });

        $(document).on("click", ".delete", function () {
            var id = $(this).data("id");
            if (id != null && id != "") {
                bootbox.confirm("Do you want to delete this ?", function (result) {
                    if (result) {
                        RB.sendAjaxRequest('/Admin/DeleteQuestionCategory', { questionCategoryId: id }, true, function (res) {
                            if (res.MessageType == "2") {
                                resetForm();
                                loadData();
                            }
                            RB.notifier(res.CurrentMessage, res.MessageType);
                        }, true, true, null);
                    }
                });
            }
        });

    };

    var loadData = function () {

        RB.sendAjaxRequest('/Admin/GetAllQuestionCategory', null, true, function (result) {
            if (result) {
                var compiled = vash.compile($('#tmpQuestionCategory').text());
                var resultText = compiled({ data: result });
                document.getElementById("div-question-category").innerHTML = resultText;
                RB.makePagination("tblQuestionCategory");
            }
        }, true, true, null);

    };

    var resetForm = function () {
        $(':input', '#' + frmId)
          .removeAttr('checked')
          .removeAttr('selected')
          .not(':button, :submit, :reset, :hidden, :radio, :checkbox')
          .val('');
    };

    var initializeForm = function () {
    };

    var init = function () {
        validateForm();
        initializeForm();
        actionHandler();
        loadData();
    };

    return {
        init: init
    };

}();