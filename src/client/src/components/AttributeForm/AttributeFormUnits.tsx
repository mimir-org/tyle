import { Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetUnits } from "api/unit.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { AttributeFormFields, unitInfoItem } from "./AttributeForm.helpers";

export interface AttributeFormUnitsProps {
  canAddUnits?: boolean;
}

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

const AttributeFormUnits = ({ canAddUnits = true }: AttributeFormUnitsProps) => {
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

export default AttributeFormUnits;
