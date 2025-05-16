interface ValidationResult {
  isValid: boolean;
  errors: Record<string, string>;
}

export const validateInput = (data: { email?: string; password?: string; confirmPassword?: string }) : ValidationResult => {
  let errors: Record<string, string> = {};
  
  // validate email
  if (!data.email) {
    errors.email = "Email can not be blank.";
  } else if (!/^\S+@\S+\.\S+$/.test(data.email)) {
    errors.email = "Invalid email.";
  }

  // validate password
  if (!data.password) {
    errors.password = "Password can not be blank.";
  } else if (data.password.length < 6) {
    errors.password = "Password must have at least 6 characters.";
  }

  // validate confirm password
  if (data.confirmPassword !== undefined) {
    if (!data.confirmPassword) {
      errors.confirmPassword = "Please re-input password.";
    } else if (data.password !== data.confirmPassword) {
      errors.confirmPassword = "Password do not match.";
    }
  }

  return {
    isValid: Object.keys(errors).length === 0,
    errors,
  };
};
