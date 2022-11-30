import { toast } from "complib/data-display";
import { useTranslation } from "react-i18next";

export const useSubmissionToast = (type: string) => {
  const { t } = useTranslation("entities");
  type = type.toLowerCase();

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("common.processing.loading", { type }),
      success: t("common.processing.success", { type }),
      error: t("common.processing.error", { type }),
    });
};
