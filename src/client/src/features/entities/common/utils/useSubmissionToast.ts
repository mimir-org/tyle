import { AxiosError } from "axios";
import { toast } from "complib/data-display";
import { useTranslation } from "react-i18next";

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
