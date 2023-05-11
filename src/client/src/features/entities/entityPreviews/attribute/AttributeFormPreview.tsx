import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormAttributeLib } from "../../attributes/types/formAttributeLib";
import AttributePreview from "./AttributePreview";

interface AttributeFormPreviewProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormPreview = ({ control }: AttributeFormPreviewProps) => {
  const { t } = useTranslation("entities");
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const attributeUnits = useWatch({ control, name: "attributeUnits" });

  return (
    <AttributePreview
      name={name ? name : t("attribute.name")}
      description={description}
      attributeUnits={attributeUnits}
    />
  );
};
