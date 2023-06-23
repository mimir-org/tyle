import { Aspect, AspectObjectTerminalLibCm } from "@mimirorg/typelibrary-types";
import {
  useCreateAspectObject,
  useGetAspectObject,
  useUpdateAspectObject,
} from "external/sources/aspectobject/aspectObject.queries";
import { AspectObjectFormPredefinedAttributes } from "features/entities/aspectobject/predefined-attributes/AspectObjectFormPredefinedAttributes";
import { AspectObjectFormTerminals } from "features/entities/aspectobject/terminals/AspectObjectFormTerminals";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";
import { UseFormResetField } from "react-hook-form";

export const useAspectObjectQuery = () => {
  const { id } = useParams();
  return useGetAspectObject(id);
};

export const useAspectObjectMutation = (id?: string, mode?: FormMode) => {
  const aspectObjectUpdateMutation = useUpdateAspectObject(id);
  const aspectObjectCreateMutation = useCreateAspectObject();
  return mode === "edit" ? aspectObjectUpdateMutation : aspectObjectCreateMutation;
};

/**
 * Resets the part of aspect object form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: UseFormResetField<FormAspectObjectLib>, newAspect: Aspect | undefined) => {
  resetField("selectedAttributePredefined", { defaultValue: [] });
  if (newAspect !== Aspect.Function && newAspect !== Aspect.Product) {
    resetField("aspectObjectTerminals", { defaultValue: [] });
  }
};

export const getSubformForAspect = (aspect: Aspect, limitedTerminals?: AspectObjectTerminalLibCm[]) => {
  switch (aspect) {
    case Aspect.Function:
      return <AspectObjectFormTerminals limitedTerminals={limitedTerminals} />;
    case Aspect.Product:
      return <AspectObjectFormTerminals limitedTerminals={limitedTerminals} />;
    case Aspect.Location:
      return <AspectObjectFormPredefinedAttributes aspects={[aspect]} />;
    default:
      return <></>;
  }
};
