document.addEventListener('DOMContentLoaded', () => {

    initOpenModals()
    initCloseButtons()
})

function clearFormErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}

function addFormErrorMessages(errors, form) {
    Object.keys(errors).forEach(key => {
        const input = form.querySelector(`[name="${key}"]`)
        if (input) {
            input.classList.add('input-validation-error')
        }

        const span = form.querySelector(`[data-valmsg-for="${key}"]`)
        if (span) {
            span.innerText = errors[key].join(' ')
            span.classList.add('field-validation-error')
        }
    })
}





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