export const onSubmitForm = <TAm, TCm>(
  submittable: TAm,
  mutate: (item: TAm) => Promise<TCm>,
  toast: (promise: Promise<unknown>) => Promise<unknown>,
) => {
  const submissionPromise = mutate(submittable);
  toast(submissionPromise);
  return submissionPromise;
};
