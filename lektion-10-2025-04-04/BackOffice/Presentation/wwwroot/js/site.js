window.addEventListener('resize', removeSidebarShowOnResize);
document.addEventListener('DOMContentLoaded', () => {
    initCloseButtons()
    initMobileMenu()
    initModals()
    initializeDropdowns();

    updateRelativeTimes();
    setInterval(updateRelativeTimes, 60000);
})

function initMobileMenu() {
    const buttons = document.querySelectorAll('[data-type="menu"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)

            targetElement.classList.add('show')
        })
    })
}

function initModals() {
    const buttons = document.querySelectorAll('[data-type="modal"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
            targetElement.classList.add('show')
        })
    })
}

function initCloseButtons() {
    const buttons = document.querySelectorAll('[data-type="close"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
            targetElement.classList.remove('show')
        })
    })
}


function removeSidebarShowOnResize() {
    const sidebar = document.getElementById('sidebar')
    if (sidebar && sidebar.classList.contains('show')) {
        sidebar.classList.remove('show')
    }
}



function initializeDropdowns() {
    const dropdownTriggers = document.querySelectorAll('[data-type="dropdown"]')

    const dropdownElements = new Set()
    dropdownTriggers.forEach(trigger => {
        const targetSelector = trigger.getAttribute('data-target')
        if (targetSelector) {
            const dropdown = document.querySelector(targetSelector)
            if (dropdown) {
                dropdownElements.add(dropdown)
            }
        }
    })

    dropdownTriggers.forEach(trigger => {
        trigger.addEventListener('click', (e) => {
            e.stopPropagation()
            const targetSelector = trigger.getAttribute('data-target')
            if (!targetSelector) return
            const dropdown = document.querySelector(targetSelector)
            if (!dropdown) return

            closeAllDropdowns(dropdown, dropdownElements)
            dropdown.classList.toggle('show')
        })
    })

    dropdownElements.forEach(dropdown => {
        dropdown.addEventListener('click', (e) => {
            e.stopPropagation()
        })
    })

    document.addEventListener('click', () => {
        closeAllDropdowns(null, dropdownElements)
    })
}

function closeAllDropdowns(exceptDropdown, dropdownElements) {
    dropdownElements.forEach(dropdown => {
        if (dropdown !== exceptDropdown) {
            dropdown.classList.remove('show')
        }
    })
}


function updateRelativeTimes() {
    const elements = document.querySelectorAll('.time');
    const now = new Date();

    elements.forEach(el => {
        const created = new Date(el.getAttribute('data-created'));
        const diff = now - created;
        const diffSeconds = Math.floor(diff / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60);
        const diffHours = Math.floor(diffMinutes / 60);
        const diffDays = Math.floor(diffHours / 24);
        const diffWeeks = Math.floor(diffDays / 7);

        let relativeTime = '';

        if (diffMinutes < 1) {
            relativeTime = 'Just now';
        } else if (diffMinutes < 60) {
            relativeTime = diffMinutes + ' min ago';
        } else if (diffHours < 2) {
            relativeTime = diffHours + ' hour ago';
        } else if (diffHours < 24) {
            relativeTime = diffHours + ' hours ago';
        } else if (diffDays < 2) {
            relativeTime = diffDays + ' day ago';
        } else if (diffDays < 7) {
            relativeTime = diffDays + ' days ago';
        } else {
            relativeTime = diffWeeks + ' weeks ago';
        }
        el.textContent = relativeTime;
    });
}

