function confirmDeleteEvent(eventId) {
    if (confirm("Are you sure you want to delete this article?")) {
        window.location.href = '/Event/Delete/' + eventId;
    }
}