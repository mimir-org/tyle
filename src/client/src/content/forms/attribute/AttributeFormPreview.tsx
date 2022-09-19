import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import { mapValueObjectsToDescriptors } from "../../../utils/mappers";
import { mapTypeReferenceAmsToDescriptors } from "../../../utils/mappers/mapTypeReferenceAmsToDescriptors";
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
  const quantityDatumSpecifiedScope = useWatch({ control, name: "quantityDatumSpecifiedScope" });
  const quantityDatumSpecifiedProvenance = useWatch({ control, name: "quantityDatumSpecifiedProvenance" });
  const quantityDatumRangeSpecifying = useWatch({ control, name: "quantityDatumRangeSpecifying" });
  const quantityDatumRegularitySpecified = useWatch({ control, name: "quantityDatumRegularitySpecified" });
  const selectValues = useWatch({ control, name: "selectValues" });
  const typeReferences = useWatch({ control, name: "typeReferences" });

  const descriptors: InfoItem[] = [
    {
      name: t("values.title"),
      descriptors: mapValueObjectsToDescriptors(selectValues),
    },
    {
      name: t("references.title"),
      descriptors: mapTypeReferenceAmsToDescriptors(typeReferences),
    },
  ];

  return (
    <AttributePreview
      variant={"large"}
      name={name ? name : t("attribute.name")}
      color={getColorFromAspect(aspect)}
      quantityDatumSpecifiedScope={quantityDatumSpecifiedScope}
      quantityDatumSpecifiedProvenance={quantityDatumSpecifiedProvenance}
      quantityDatumRangeSpecifying={quantityDatumRangeSpecifying}
      quantityDatumRegularitySpecified={quantityDatumRegularitySpecified}
      contents={descriptors}
    />
  );
};
