import { Trash } from "@styled-icons/heroicons-outline";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Flexbox } from "../../../../complib/layouts";
import { useGetUnits } from "../../../../data/queries/tyle/queriesUnit";
import { InfoItemButton } from "../../../common/info-item";
import { FormSection } from "../../common/form-section/FormSection";
import { SelectItemDialog } from "../../common/select-item-dialog/SelectItemDialog";
import { FormAttributeLib } from "../types/formAttributeLib";
import { getSelectItemsFromUnitsLibCms, onAddUnits } from "./AttributeFormUnits.helpers";

export const AttributeFormUnits = () => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "units" });
  const { control, register } = useFormContext<FormAttributeLib>();

  const unitQuery = useGetUnits();
  const unitFields = useFieldArray({ control, name: "unitIdList" });
  const unitItems = getSelectItemsFromUnitsLibCms(unitQuery.data);

  return (
    <FormSection
      title={t("title")}
      action={
        <SelectItemDialog
          title={t("dialog.title")}
          description={t("dialog.description")}
          searchFieldText={t("dialog.search")}
          addItemsButtonText={t("dialog.add")}
          openDialogButtonText={t("open")}
          items={unitItems}
          onAdd={(ids) => onAddUnits(ids, unitFields)}
        />
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
        {unitFields.fields.map((field, index) => {
          const attribute = unitItems.find((x) => x.id === field.value);
          return (
            attribute && (
              <InfoItemButton
                key={field.id}
                {...register(`unitIdList.${index}`)}
                {...attribute}
                actionable
                actionIcon={<Trash />}
                actionText={t("remove")}
                onAction={() => unitFields.remove(index)}
              />
            )
          );
        })}
      </Flexbox>
    </FormSection>
  );
};
