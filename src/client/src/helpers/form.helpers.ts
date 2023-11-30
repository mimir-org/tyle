import { toast } from "@mimirorg/component-library";
import { UseQueryResult } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

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

  return [isPrefilled, query.isInitialLoading];
};

export const useSubmissionToast = (type: string) => {
  const { t } = useTranslation("entities");
  type = type.toLowerCase();

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("common.processing.loading", { type }),
      success: t("common.processing.success", { type }),
      error: (error: AxiosError) => {
        if (error.response?.status === 403) return t("common.processing.error.403", { data: error.response?.data });
        return t("common.processing.error.default", { type });
      },
    });
};
