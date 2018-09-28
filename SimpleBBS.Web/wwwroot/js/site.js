// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addReply(ele, url) {
    var content = $(ele).closest('#replyForm').find('[name=content]').val();
    var parentId = $(ele).closest('#replyForm').find('[name=parentId]').val();

    if (content.length === 0) {
        alert('请输入回复内容')
        return;
    }

    $.post(url, { content: content, parentId: parentId }, function (res) {
        if (res.result) {
            location.reload();
        } else {
            alert('回复失败，请稍后再试');
        }
    });

}