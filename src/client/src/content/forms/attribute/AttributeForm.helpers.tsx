import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { toast } from "../../../complib/data-display";
import { useGetAttribute } from "../../../data/queries/tyle/queriesAttribute";
import { FormAttributeLib, mapAttributeLibCmToFormAttributeLib } from "./types/formAttributeLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledAttributeData = (
  reset: (values?: DefaultValues<FormAttributeLib> | FormAttributeLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const attributeQuery = useGetAttribute(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && attributeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapAttributeLibCmToFormAttributeLib(attributeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, attributeQuery.isSuccess, attributeQuery.data, reset]);

  return [hasPrefilled, attributeQuery.isLoading];
};

export const useAttributeSubmissionToast = () => {
  const { t } = useTranslation("translation", { keyPrefix: "attribute.processing" });

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("loading"),
      success: t("success"),
      error: t("error"),
    });
};

export const prepareParentAttributes = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return attributes.filter((a) => a.parentName === null);
};
