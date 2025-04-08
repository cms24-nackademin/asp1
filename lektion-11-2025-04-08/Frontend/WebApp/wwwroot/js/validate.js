function showValidationErrors(errors) {
    for (const field in errors) {
        const messages = errors[field];
        const fieldElement = document.querySelector(`[name="${field}"]`);

        if (fieldElement) {
            let errorElement = fieldElement.parentElement.querySelector(".field-validation-error");
            if (!errorElement) {
                errorElement = document.createElement("div");
                errorElement.className = "field-validation-error";
                fieldElement.parentElement.appendChild(errorElement);
            }
            errorElement.innerHTML = messages.join("<br/>");
        }
    }
}