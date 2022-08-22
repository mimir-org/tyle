import { Aspect, AttributeLibCm, AttributeType } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { Control, DefaultValues, KeepStateOptions, UseFormRegister } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { toast } from "../../../complib/data-display";
import { FormAttributeLib } from "./types/formAttributeLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledAttributeData = (
  reset: (values?: DefaultValues<FormAttributeLib> | FormAttributeLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  // const { id } = useParams();
  // const nodeQuery = useGetNode(id);
  // const [hasPrefilled, setHasPrefilled] = useState(false);

  // useEffect(() => {
  //   if (!hasPrefilled && nodeQuery.isSuccess) {
  //     setHasPrefilled(true);
  //     reset(mapNodeLibCmToFormNodeLib(nodeQuery.data), { keepDefaultValues: false });
  //   }
  // }, [hasPrefilled, nodeQuery.isSuccess, nodeQuery.data, reset]);

  return [false, false];
};

/**
 * Resets the part of node form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormAttributeLib) => void) => {
  resetField("name");
};

export const useNodeSubmissionToast = () => {
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

// export const getFormForAspect = (
//   aspect: Aspect,
//   control: Control<FormAttributeLib>,
//   register: UseFormRegister<FormAttributeLib>
// ) => {
//   switch (aspect) {
//     case Aspect.Function:
//       return <FunctionNode control={control} register={register} />;
//     case Aspect.Product:
//       return <ProductNode control={control} register={register} />;
//     case Aspect.Location:
//       return <LocationNode control={control} register={register} />;
//     default:
//       return <></>;
//   }
// };
