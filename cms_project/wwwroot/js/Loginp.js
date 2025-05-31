document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('form[asp-action="Login"]');
    const emailInput = form.querySelector('input[asp-for="Email"]');
    const passwordInput = form.querySelector('input[asp-for="Password"]');

    // Cache validation message spans (assumed to be immediate next sibling)
    const emailFeedback = emailInput.nextElementSibling;
    const passwordFeedback = passwordInput.nextElementSibling;

    // Utility: set error state
    function setError(input, feedbackElem, message) {
        input.classList.add('is-invalid');
        input.classList.remove('is-valid');
        if (feedbackElem) feedbackElem.textContent = message;
    }

    // Utility: set success state
    function setSuccess(input, feedbackElem) {
        input.classList.remove('is-invalid');
        input.classList.add('is-valid');
        if (feedbackElem) feedbackElem.textContent = '';
    }

    // Validate email with regex
    function validateEmail() {
        const email = emailInput.value.trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!email) {
            setError(emailInput, emailFeedback, "Email is required.");
            return false;
        }
        if (!emailRegex.test(email)) {
            setError(emailInput, emailFeedback, "Please enter a valid email address.");
            return false;
        }

        setSuccess(emailInput, emailFeedback);
        return true;
    }

    // Validate password: required, min length, at least one digit
    function validatePassword() {
        const password = passwordInput.value.trim();

        if (!password) {
            setError(passwordInput, passwordFeedback, "Password is required.");
            return false;
        }
        if (password.length < 4) {
            setError(passwordInput, passwordFeedback, "Password must be at least 4 characters long.");
            return false;
        }
        if (!/\d/.test(password)) {
            setError(passwordInput, passwordFeedback, "Password must contain at least one digit.");
            return false;
        }

        setSuccess(passwordInput, passwordFeedback);
        return true;
    }

    // Form submit handler
    form.addEventListener('submit', e => {
        e.preventDefault();

        const emailValid = validateEmail();
        const passwordValid = validatePassword();

        if (emailValid && passwordValid) {
            // Optionally disable submit button here to prevent multiple submits

            // Submit form programmatically, but avoid infinite loop
            form.removeEventListener('submit', arguments.callee);
            form.submit();
        }
    });

    // Live validation on input
    emailInput.addEventListener('input', validateEmail);
    passwordInput.addEventListener('input', validatePassword);
});
