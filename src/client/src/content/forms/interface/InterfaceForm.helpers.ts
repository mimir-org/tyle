import { Aspect, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { toast } from "../../../complib/data-display";
import { useGetInterface } from "../../../data/queries/tyle/queriesInterface";
import { FormInterfaceLib, mapInterfaceLibCmToFormInterfaceLib } from "./types/formInterfaceLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes interface data as parameter and populates form
 */
export const usePrefilledInterfaceData = (
  reset: (values?: DefaultValues<FormInterfaceLib> | FormInterfaceLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const interfaceQuery = useGetInterface(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && interfaceQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapInterfaceLibCmToFormInterfaceLib(interfaceQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, interfaceQuery.isSuccess, interfaceQuery.data, reset]);

  return [hasPrefilled, interfaceQuery.isLoading];
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributeIdList");
};

export const useInterfaceSubmissionToast = () => {
  const { t } = useTranslation("translation", { keyPrefix: "interface.processing" });

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("loading"),
      success: t("success"),
      error: t("error"),
    });
};

export const prepareAttributes = (attributes?: AttributeLibCm[], aspects?: Aspect[]) => {
  if (!attributes || attributes.length == 0) return [];
  if (!aspects || aspects.length == 0) return [];

  return attributes.filter((a) => aspects.some((x) => x === a.aspect)).sort((a, b) => a.discipline - b.discipline);
};
