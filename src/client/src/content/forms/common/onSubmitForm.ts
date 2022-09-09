import { UpdateEntity } from "../../../data/types/updateEntity";

export const onSubmitForm = <TAm, TCm>(
  submittable: UpdateEntity<TAm>,
  mutate: (item: UpdateEntity<TAm>) => Promise<TCm>,
  toast: (promise: Promise<unknown>) => Promise<unknown>
) => {
  const submissionPromise = mutate(submittable);
  toast(submissionPromise);
  return submissionPromise;
};
