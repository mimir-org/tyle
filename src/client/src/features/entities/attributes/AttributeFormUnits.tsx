import {
  Button,
  Checkbox,
  ConditionalWrapper,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Select,
  Textarea,
  Token,
  VisuallyHidden,
} from "@mimirorg/component-library";
import { Controller, useFieldArray, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { AttributeFormFields, unitInfoItem } from "./AttributeForm.helpers";
import { ConstraintType } from "common/types/attributes/constraintType";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { XsdDataType } from "common/types/attributes/xsdDataType";
import { FormSection } from "../common/form-section/FormSection";
import { PlusSmall, Trash, XCircle } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { useGetUnits } from "external/sources/unit/unit.queries";
import { SelectItemDialog } from "../common/select-item-dialog/SelectItemDialog";

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
    setValue,
    formState: { errors },
  } = useFormContext<AttributeFormFields>();

  const unitFields = useFieldArray({ control, name: "units" });
  const unitQuery = useGetUnits();
  const unitInfoItems = unitQuery.data?.map((p) => unitInfoItem(p)) ?? [];

  return (
    <FormSection
      title={t("attribute.units.title")}
      error={errors.units}
      action={
        canAddUnits && (
          <SelectItemDialog
            title="Add units"
            description="A unit is nice to have"
            searchFieldText="Find units"
            addItemsButtonText="Add"
            openDialogButtonText="test"
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
          actionText={t("remove")}
          onAction={() => unitFields.remove(index)}
          dangerousAction
        >
          {field.name}
        </Token>
      ))}
    </FormSection>
  );
};
