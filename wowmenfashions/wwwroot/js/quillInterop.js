window.quillInterop = {
    editors: {},
    init: function (elementId, dotNetRef, initialContent) {
        if (!document.getElementById(elementId)) return;
        
        var quill = new Quill('#' + elementId, {
            theme: 'snow',
            modules: {
                toolbar: [
                    [{ 'header': [1, 2, 3, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                    [{ 'color': [] }, { 'background': [] }],
                    ['link', 'clean']
                ]
            }
        });
        
        if (initialContent) {
            quill.clipboard.dangerouslyPasteHTML(initialContent);
        }

        quill.on('text-change', function() {
            var html = quill.root.innerHTML;
            dotNetRef.invokeMethodAsync('UpdateContent', html);
        });

        this.editors[elementId] = quill;
    },
    getHtml: function (elementId) {
        var quill = this.editors[elementId];
        return quill ? quill.root.innerHTML : '';
    },
    setHtml: function (elementId, html) {
        var quill = this.editors[elementId];
        if (quill) {
            quill.clipboard.dangerouslyPasteHTML(html);
        }
    }
};
