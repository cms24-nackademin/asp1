(function () {
    try {
        if (localStorage.getItem('theme') === 'dark') {
            document.documentElement.setAttribute('data-mode', 'dark')
        }
    }
    catch { }
})()

document.addEventListener('DOMContentLoaded', () => {
    initDarkMode()
    initDarkToggle()
})

function initDarkMode() {
    if (localStorage.getItem('theme') === 'dark') {
        document.body.classList.add('dark-mode')
        document.documentElement.setAttribute('data-mode', 'dark')

        const darkModeToggle = document.querySelector('#darkModeToggle')
        darkModeToggle.checked = true
    }
}

function initDarkToggle() {
    const toggles = document.querySelectorAll('[data-type="toggle"]')

    toggles.forEach(toggle => {
        const togglefunction = toggle.getAttribute('data-func')

        if (togglefunction === "darkmode") {
            toggle.addEventListener('change', function () {
                if (this.checked) {
                    document.documentElement.setAttribute('data-mode', 'dark')
                    localStorage.setItem('theme', 'dark')
                } else {
                    document.documentElement.removeAttribute('data-mode')
                    localStorage.setItem('theme', 'light')
                }
            });
        }
    })
}