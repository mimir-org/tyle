import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import { mapTypeReferenceCmsToDescriptors, mapValueObjectsToDescriptors } from "../../../utils/mappers";
import { AttributePreview } from "../../common/attribute";
import { InfoItem } from "../../types/InfoItem";
import { FormAttributeLib } from "./types/formAttributeLib";

interface AttributeFormPreviewProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormPreview = ({ control }: AttributeFormPreviewProps) => {
  const { t } = useTranslation();
  const name = useWatch({ control, name: "name" });
  const aspect = useWatch({ control, name: "aspect" });
  const source = useWatch({ control, name: "attributeSource" });
  const qualifier = useWatch({ control, name: "attributeQualifier" });
  const condition = useWatch({ control, name: "attributeCondition" });
  const selectValues = useWatch({ control, name: "selectValues" });
  const typeReferences = useWatch({ control, name: "typeReferences" });

  const descriptors: InfoItem[] = [
    {
      name: t("values.title"),
      descriptors: mapValueObjectsToDescriptors(selectValues),
    },
    {
      name: t("references.title"),
      descriptors: mapTypeReferenceCmsToDescriptors(typeReferences),
    },
  ];

  return (
    <AttributePreview
      variant={"large"}
      name={name ? name : t("attribute.name")}
      color={getColorFromAspect(aspect)}
      qualifier={qualifier}
      source={source}
      condition={condition}
      contents={descriptors}
    />
  );
};
