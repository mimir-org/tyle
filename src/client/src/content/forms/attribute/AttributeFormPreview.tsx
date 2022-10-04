import { Control, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import {
  mapAttributeLibToQuantityDatumDescriptors,
  mapTypeReferencesToDescriptors,
  mapValueObjectsToDescriptors,
} from "../../../utils/mappers";
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
  const selectValues = useWatch({ control, name: "selectValues" });
  const typeReferences = useWatch({ control, name: "typeReferences" });
  const quantityDatumSpecifiedScope = useWatch({ control, name: "quantityDatumSpecifiedScope" });
  const quantityDatumRangeSpecifying = useWatch({ control, name: "quantityDatumRangeSpecifying" });
  const quantityDatumSpecifiedProvenance = useWatch({ control, name: "quantityDatumSpecifiedProvenance" });
  const quantityDatumRegularitySpecified = useWatch({ control, name: "quantityDatumRegularitySpecified" });

  const descriptors: InfoItem[] = [
    {
      name: t("values.title"),
      descriptors: mapValueObjectsToDescriptors(selectValues),
    },
    {
      name: t("datum.title"),
      descriptors: mapAttributeLibToQuantityDatumDescriptors({
        quantityDatumRegularitySpecified,
        quantityDatumSpecifiedProvenance,
        quantityDatumRangeSpecifying,
        quantityDatumSpecifiedScope,
      }),
    },
    {
      name: t("references.title"),
      descriptors: mapTypeReferencesToDescriptors(typeReferences),
    },
  ];

  return (
    <AttributePreview
      variant={"large"}
      name={name ? name : t("attribute.name")}
      color={getColorFromAspect(aspect)}
      contents={descriptors}
    />
  );
};
