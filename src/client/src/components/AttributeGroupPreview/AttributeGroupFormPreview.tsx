import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormAttributeGroupLib } from "../AttributeGroupForm/formAttributeGroupLib";
import AttributeGroupPreview from "./AttributeGroupPreview";
import { StyledFormPreviewDiv } from "./FormPreviewContainer";

interface AttributeGroupFormPreviewProps {
  control: Control<FormAttributeGroupLib>;
}

export const AttributeGroupFormPreview = ({ control }: AttributeGroupFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });

  return (
    <StyledFormPreviewDiv>
      <AttributeGroupPreview name={name ? name : t("attributeGroup.name")} description={description} />
    </StyledFormPreviewDiv>
  );
};
