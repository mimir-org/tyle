import { Aspect } from "@mimirorg/typelibrary-types";
import {
  useCreateAspectObject,
  useGetAspectObject,
  useUpdateAspectObject,
} from "external/sources/aspectobject/aspectObject.queries";
import { AspectObjectFormPredefinedAttributes } from "features/entities/aspectobject/predefined-attributes/AspectObjectFormPredefinedAttributes";
import { AspectObjectFormTerminals } from "features/entities/aspectobject/terminals/AspectObjectFormTerminals";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { AspectObjectFormMode } from "features/entities/aspectobject/types/aspectObjectFormMode";
import { useParams } from "react-router-dom";

export const useAspectObjectQuery = () => {
  const { id } = useParams();
  return useGetAspectObject(id);
};

export const useAspectObjectMutation = (id?: string, mode?: AspectObjectFormMode) => {
  const aspectObjectUpdateMutation = useUpdateAspectObject(id);
  const aspectObjectCreateMutation = useCreateAspectObject();
  return mode === "edit" ? aspectObjectUpdateMutation : aspectObjectCreateMutation;
};

/**
 * Resets the part of aspect object form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormAspectObjectLib) => void) => {
  resetField("selectedAttributePredefined");
  resetField("aspectObjectTerminals");
  resetField("attributes");
};

export const getSubformForAspect = (aspect: Aspect, mode?: AspectObjectFormMode) => {
  switch (aspect) {
    case Aspect.Function:
      return <AspectObjectFormTerminals canRemoveTerminals={mode !== "edit"} />;
    case Aspect.Product:
      return <AspectObjectFormTerminals canRemoveTerminals={mode !== "edit"} />;
    case Aspect.Location:
      return <AspectObjectFormPredefinedAttributes aspects={[aspect]} />;
    default:
      return <></>;
  }
};
