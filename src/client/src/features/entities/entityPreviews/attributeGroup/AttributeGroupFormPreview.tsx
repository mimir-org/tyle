import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormAttributeGroupLib } from "../../attributeGroups/types/formAttributeGroupLib";
import AttributeGroupPreview from "./AttributeGroupPreview";
import { StyledFormPreviewDiv } from "../FormPreviewContainer";
import { size } from "polished";

interface AttributeGroupFormPreviewProps {
  control: Control<FormAttributeGroupLib>;
}

export const AttributeGroupFormPreview = ({ control }: AttributeGroupFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  //  const units = useWatch({ control, name: "units" });
  //  const defaultUnit = useWatch({ control, name: "defaultUnit" });

  return (
    <StyledFormPreviewDiv>
      <AttributeGroupPreview
        name={name ? name : t("attributeGroup.name")}
        description={description}
        // units={units}
        // defaultUnit={defaultUnit}
      />
    </StyledFormPreviewDiv>
  );
};