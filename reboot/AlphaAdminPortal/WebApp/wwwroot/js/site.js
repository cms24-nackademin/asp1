document.addEventListener('DOMContentLoaded', () => {

    initOpenModals()
    initCloseButtons()
})

function initOpenModals() {
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const modal = document.querySelector(target)

            if (modal) {
                modal.classList.add('flex')
            }
        })
    })
}

function initCloseButtons() {
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {

        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)

            if (targetElement) {
                if (targetElement.classList.contains('modal')) {
                    closeModal(targetElement)
                }
            }
        })
    })
}

function closeModal(modal) {
    if (modal) {
        modal.classList.remove('flex')

        modal.querySelectorAll('form').forEach(form => {
            form.reset()
        })
    }
}