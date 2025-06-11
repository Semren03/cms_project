document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('loginForm');
    const emailInput = form.querySelector('input[asp-for="Email"]');
    const passwordInput = form.querySelector('input[asp-for="Password"]');
    const loginButton = document.getElementById('loginButton');

    // Get validation message spans
    const emailFeedback = emailInput.parentElement.querySelector('.validation-message');
    const passwordFeedback = passwordInput.parentElement.querySelector('.validation-message');

    // Utility: set error state
    function setError(input, feedbackElem, message) {
        input.classList.add('is-invalid');
        input.classList.remove('is-valid');
        if (feedbackElem) {
            feedbackElem.textContent = message;
            feedbackElem.style.display = 'block';
        }
    }

    // Utility: set success state
    function setSuccess(input, feedbackElem) {
        input.classList.remove('is-invalid');
        input.classList.add('is-valid');
        if (feedbackElem) {
            feedbackElem.textContent = '';
            feedbackElem.style.display = 'none';
        }
    }

    // Utility: clear validation state
    function clearValidation(input, feedbackElem) {
        input.classList.remove('is-invalid', 'is-valid');
        if (feedbackElem) {
            feedbackElem.textContent = '';
            feedbackElem.style.display = 'none';
        }
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

    // Show loading state
    function showLoading() {
        loginButton.classList.add('loading');
        loginButton.disabled = true;
        loginButton.textContent = 'LOGGING IN...';
    }

    // Hide loading state
    function hideLoading() {
        loginButton.classList.remove('loading');
        loginButton.disabled = false;
        loginButton.textContent = 'LOG IN';
    }

    // Form submit handler
    form.addEventListener('submit', function (e) {
        e.preventDefault();

        // Clear any existing server-side validation messages
        const serverErrors = form.querySelectorAll('.alert-danger');
        serverErrors.forEach(error => error.remove());

        const emailValid = validateEmail();
        const passwordValid = validatePassword();

        if (emailValid && passwordValid) {
            showLoading();

            // Submit form after a short delay to show loading state
            setTimeout(() => {
                // Remove event listener to prevent multiple submissions
                form.removeEventListener('submit', arguments.callee);
                form.submit();
            }, 500);
        } else {
            // Focus on first invalid field
            if (!emailValid) {
                emailInput.focus();
            } else if (!passwordValid) {
                passwordInput.focus();
            }
        }
    });

    // Live validation on input
    emailInput.addEventListener('input', function () {
        if (this.value.trim() === '') {
            clearValidation(this, emailFeedback);
        } else {
            validateEmail();
        }
    });

    passwordInput.addEventListener('input', function () {
        if (this.value.trim() === '') {
            clearValidation(this, passwordFeedback);
        } else {
            validatePassword();
        }
    });

    // Clear validation on focus
    emailInput.addEventListener('focus', function () {
        if (!this.value.trim()) {
            clearValidation(this, emailFeedback);
        }
    });

    passwordInput.addEventListener('focus', function () {
        if (!this.value.trim()) {
            clearValidation(this, passwordFeedback);
        }
    });

    // Prevent form submission on Enter if validation fails
    form.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            form.dispatchEvent(new Event('submit'));
        }
    });

    // Handle browser back button or page reload
    window.addEventListener('beforeunload', function () {
        hideLoading();
    });
});