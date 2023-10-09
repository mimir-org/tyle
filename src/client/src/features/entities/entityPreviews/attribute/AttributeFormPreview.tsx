import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { AttributeFormFields } from "../../attributes/types/formAttributeLib";
import AttributePreview from "./AttributePreview";
import { StyledFormPreviewDiv } from "../FormPreviewContainer";

interface AttributeFormPreviewProps {
  control: Control<AttributeFormFields>;
}

export const AttributeFormPreview = ({ control }: AttributeFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const units = useWatch({ control, name: "units" });
  const defaultUnit = useWatch({ control, name: "defaultUnit" });

  return (
    <StyledFormPreviewDiv>
      <AttributePreview
        name={name ? name : t("attribute.name")}
        description={description}
        units={units}
        defaultUnit={defaultUnit}
      />
    </StyledFormPreviewDiv>
  );
};
