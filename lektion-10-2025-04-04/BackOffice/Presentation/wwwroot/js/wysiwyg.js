function initWysiwygEditor(editorId, toolbarId, textareaId, content) {
    const textarea = document.querySelector(textareaId)

    const quill = new Quill(editorId, {
        modules: {
            syntax: true,
            toolbar: toolbarId
        },
        placeholder: 'Type something',
        theme: 'snow'
    })

    if (content) {
        quill.root.innerHTML = content
    }

    quill.on('text-change', () => {
        textarea.value = quill.root.innerHTML
    })
}