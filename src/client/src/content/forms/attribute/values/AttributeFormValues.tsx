import { Trash } from "@styled-icons/heroicons-outline";
import { Control, Controller, useFieldArray } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../../complib/buttons";
import { Input } from "../../../../complib/inputs";
import { Flexbox } from "../../../../complib/layouts";
import { FormSection } from "../../common/FormSection";
import { FormAttributeLib } from "../types/formAttributeLib";
import { AttributeFormValueAddButton } from "./AttributeFormValueAddButton";

export interface AttributeFormValuesProps {
  control: Control<FormAttributeLib>;
}

export const AttributeFormValues = ({ control }: AttributeFormValuesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "attribute.values" });
  const valueFields = useFieldArray({ control, name: "selectValues" });
  const onRemove = (index: number) => valueFields.remove(index);

  return (
    <FormSection
      title={t("title")}
      action={<AttributeFormValueAddButton onClick={() => valueFields.append({ value: "" })} />}
    >
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        {valueFields.fields.map((field, index) => (
          <Controller
            key={field.id}
            control={control}
            name={`selectValues.${index}`}
            render={({ field: { value, ...rest } }) => {
              return (
                <Flexbox gap={theme.tyle.spacing.base} alignItems={"center"}>
                  <Button variant={"outlined"} icon={<Trash />} iconOnly onClick={() => onRemove(index)}>
                    {t("remove")}
                  </Button>
                  <Input {...rest} id={field.id} value={value.value} placeholder={t("placeholders.value")} />
                </Flexbox>
              );
            }}
          />
        ))}
      </Flexbox>
    </FormSection>
  );
};
