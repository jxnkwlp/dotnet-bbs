/// <reference path="../lib/jquery/dist/jquery.min.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitReply(ele, url) {
    var content = $(ele).closest('.reply-form').find('[name=content]').val();
    var parentId = $(ele).closest('.reply-form').find('[name=parentId]').val();

    if (content.length === 0) {
        alert('请输入回复内容')
        return;
    }

    $.post(url, { content: content, parentId: parentId }).done(function (data) {
        if (data.result) {
            location.reload();
        } else {
            alert('回复失败，请稍后再试');
        }
    }).fail(function (res) {
        if (res.status == 401) {
            alert('请先登录');
        }
    });

}

function cancelReply(ele) {
    $(ele).closest('.reply-form').remove();
}

function insertReplyForm(ele) {
    var $item = $(ele).closest('.reply-item');
    var currentId = $(ele).closest('.reply-item').data('id');

    if ($item.find('.reply-form').length > 0) {
        return;
    }

    var html = $('#replyForm').html();
    var $form = $('<div class="reply-form" />').append(html);

    $form.find('.btn-cancel').css('display', 'initial');
    $form.find('[name=parentId]').val(currentId);

    $form.find('[name=content]').parent().attr('id', 'content_body_' + currentId);
    $form.find('[name=content]').attr('id', 'content_' + currentId);

    $item.append($form);

    var mde = addMDE('content_' + currentId, 300, true);
}



function deleteTopic(url) {
    if (confirm('确定要删除吗？')) {
        $.post(url).done(function (data) {
            if (data.result) {
                location.href = '/';
            } else {
                alert('删除失败，请稍后再试');
            }
        })
    }
}

function addMDE(eleID, height, simple) {
    //return new SimpleMDE({
    //    element: $(ele)[0],
    //    spellChecker: false,
    //    promptURLs: true,
    //});

    if (!height) {
        height = 600;
    }

    var node = document.getElementById(eleID);
    if (node.tagName == 'TEXTAREA') {
        eleID = node.parentElement.id;
    }

    return editormd(eleID, {
        path: '/lib/editor.md/lib/',

        toolbarIcons: function () {
            if (simple)
                return editormd.toolbarModes['simple'];
            else
                return editormd.toolbarModes['full'];
        }, 
        height: height,
        watch: false,
        emoji: true,

        imageUpload: true,
        imageFormats: ["jpg", "jpeg", "gif", "png", "bmp", "webp"],
        imageUploadURL: "./php/upload.php",

    });

}