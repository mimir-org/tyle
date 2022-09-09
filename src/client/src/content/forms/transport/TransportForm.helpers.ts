import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { toast } from "../../../complib/data-display";
import { useGetTransport } from "../../../data/queries/tyle/queriesTransport";
import { FormTransportLib, mapTransportLibCmToFormTransportLib } from "./types/formTransportLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes transport data as parameter and populates form
 */
export const usePrefilledTransportData = (
  reset: (values?: DefaultValues<FormTransportLib> | FormTransportLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const transportQuery = useGetTransport(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && transportQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapTransportLibCmToFormTransportLib(transportQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, transportQuery.isSuccess, transportQuery.data, reset]);

  return [hasPrefilled, transportQuery.isLoading];
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormTransportLib) => void) => {
  resetField("attributeIdList");
};

export const useTransportSubmissionToast = () => {
  const { t } = useTranslation("translation", { keyPrefix: "transport.processing" });

  return (submissionPromise: Promise<unknown>) =>
    toast.promise(submissionPromise, {
      loading: t("loading"),
      success: t("success"),
      error: t("error"),
    });
};

