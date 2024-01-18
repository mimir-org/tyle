import { UseQueryResult } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { toast } from "components/Toaster/toast";
import { useEffect, useState } from "react";

export const onSubmitForm = <TAm, TCm>(
  submittable: TAm,
  mutate: (item: TAm) => Promise<TCm>,
  toast: (promise: Promise<unknown>) => Promise<unknown>,
) => {
  const submissionPromise = mutate(submittable);
  toast(submissionPromise);
  return submissionPromise;
};

export const usePrefilledForm = <TIn, TOut>(
  query: UseQueryResult<TIn>,
  mapQueryDataToFormModel: (data: TIn) => TOut,
  populateForm: (values: TOut) => void,
): [isPrefilled: boolean, isLoading: boolean] => {
  const [isPrefilled, setIsPrefilled] = useState(false);

  useEffect(() => {
    if (!isPrefilled && query.isSuccess) {
      setIsPrefilled(true);
      populateForm(mapQueryDataToFormModel(query.data));
    }
  }, [query.data, query.isSuccess, populateForm, isPrefilled, mapQueryDataToFormModel]);

  return [isPrefilled, query.isLoading];
};

export const useSubmissionToast = (type: string) => {
  type = type.toLowerCase();

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: `Submitting ${type}`,
      success: `Your ${type} has been submitted`,
      error: (error: AxiosError) => {
        if (error.response?.status === 403) return `403 (Forbidden) error: ${error.response?.data}`;
        return `An error occurred during ${type} submission`;
      },
    });
};
