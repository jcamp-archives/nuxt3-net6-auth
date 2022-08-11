export const valRules = {
  mfaCode: 'required|length:6,7',
  password:
    'required|length:10|matches:/^.*(?=.{8,})((?=.*[!@#$%^&*()-_=+{};:,<.>]){1})(?=.*d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$/',
  passwordMessages: {
    matches:
      'Password must contain one uppercase, one number and one special case character.',
  },
  required: 'required|',
}
