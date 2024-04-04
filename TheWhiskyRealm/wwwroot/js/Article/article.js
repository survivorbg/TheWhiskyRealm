function confirmDelete(articleId) {
    if (confirm("Are you sure you want to delete this article?")) {
        window.location.href = '/Article/Delete/' + articleId;
    }
}
function confirmDeleteComment(commentId) {
    if (confirm("Are you sure you want to delete this article?")) {
        window.location.href = '/Comment/Delete/' + commentId;
    }
}
