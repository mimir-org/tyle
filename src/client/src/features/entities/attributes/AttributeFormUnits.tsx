import { Token } from "@mimirorg/component-library";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { AttributeFormFields, unitInfoItem } from "./AttributeForm.helpers";
import { FormSection } from "../common/form-section/FormSection";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetUnits } from "external/sources/unit/unit.queries";
import { SelectItemDialog } from "../common/select-item-dialog/SelectItemDialog";
import { RdlUnit } from "common/types/attributes/rdlUnit";

export interface AttributeFormUnitsProps {
  canAddUnits?: boolean;
}

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const AttributeFormUnits = ({ canAddUnits = true }: AttributeFormUnitsProps) => {
  const { t } = useTranslation("entities");
  const {
    control,
    register,
    formState: { errors },
  } = useFormContext<AttributeFormFields>();

  const unitFields = useFieldArray({ control: control, name: "units" });
  const unitQuery = useGetUnits();
  const unitInfoItems = unitQuery.data?.map((p) => unitInfoItem(p)) ?? [];

  return (
    <FormSection
      title={t("attribute.units.title")}
      error={errors.units}
      action={
        canAddUnits && (
          <SelectItemDialog
            title={t("attribute.units.dialog.title")}
            description={t("attribute.units.dialog.description")}
            searchFieldText={t("attribute.units.dialog.search")}
            addItemsButtonText={t("attribute.units.dialog.add")}
            openDialogButtonText={t("attribute.units.dialog.open")}
            items={unitInfoItems}
            onAdd={(ids) => {
              ids.forEach((id) => {
                const targetUnit = unitQuery.data?.find((x) => x.id === Number(id));
                if (targetUnit) unitFields.append(targetUnit);
              });
            }}
          />
        )
      }
    >
      {unitFields.fields.map((field, index) => (
        <Token
          variant={"secondary"}
          key={field.id}
          {...register(`units.${index}`)}
          actionable
          actionIcon={<XCircle />}
          actionText={t("attribute.units.remove")}
          onAction={() => unitFields.remove(index)}
          dangerousAction
        >
          {field.name}
        </Token>
      ))}
    </FormSection>
  );
};
