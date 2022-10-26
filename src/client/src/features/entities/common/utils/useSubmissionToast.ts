import { useTranslation } from "react-i18next";
import { toast } from "../../../../complib/data-display";

export const useSubmissionToast = (type: string) => {
  const { t } = useTranslation("translation", { keyPrefix: "processing" });
  type = type.toLowerCase();

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("loading", { type }),
      success: t("success", { type }),
      error: t("error", { type }),
    });
};
