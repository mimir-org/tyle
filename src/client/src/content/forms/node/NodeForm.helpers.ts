import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { toast } from "../../../complib/data-display";
import { useGetNode } from "../../../data/queries/tyle/queriesNode";
import { FormNodeLib, mapNodeLibCmToFormNodeLib } from "../types/formNodeLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledNodeData = (
  reset: (values?: DefaultValues<FormNodeLib> | FormNodeLib, keepStateOptions?: KeepStateOptions) => void
) => {
  const { id } = useParams();
  const nodeQuery = useGetNode(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && nodeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapNodeLibCmToFormNodeLib(nodeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, nodeQuery.isSuccess, nodeQuery.data, reset]);

  return hasPrefilled;
};

/**
 * Resets the part of node form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormNodeLib) => void) => {
  resetField("selectedAttributePredefined");
  resetField("nodeTerminals");
  resetField("attributeIdList");
};

export const useNodeSubmissionToast = () => {
  const { t } = useTranslation("translation", { keyPrefix: "node.processing" });

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("loading"),
      success: t("success"),
      error: t("error"),
    });
};
