import { XCircle } from "@styled-icons/heroicons-outline";
import { Box, Checkbox, Counter, Flexbox, Token } from "@mimirorg/component-library";
import { useGetAttributes } from "external/sources/attribute/attribute.queries";
import {
  onAddAttributes,
  resolveSelectedAndAvailableAttributes,
} from "features/entities/common/form-attributes/FormAttributes.helpers";
import { FormSection } from "features/entities/common/form-section/FormSection";
import { SelectItemDialog } from "features/entities/common/select-item-dialog/SelectItemDialog";
import { Controller, useFieldArray, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { TerminalFormFields } from "features/entities/terminal/TerminalForm.helpers";
import { prepareAttributes } from "../utils/prepareAttributes";
import { BlockFormFields } from "features/entities/block/BlockForm.helpers";

/**
 * Reusable form section for adding attributes to models that support them
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param preprocess pass a function to alter the attribute data before it is shown to the user
 * @param canAddAttributes controls if the add action is shown
 * @param canRemoveAttributes controls if the remove action is shown
 * @param limitedAttributes attributes that cannot be removed, even if removing attributes is allowed
 * @constructor
 */
export const FormAttributes = () => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  type BlockOrTerminalFormFields = BlockFormFields | TerminalFormFields;

  const { control, register, setValue } = useFormContext<BlockOrTerminalFormFields>();

  const attributeFields = useFieldArray({ control, name: "attributes" });
  const attributeQuery = useGetAttributes();
  const attributes = prepareAttributes(attributeQuery.data) ?? [];
  const [available, selected] = resolveSelectedAndAvailableAttributes(attributeFields.fields, attributes);
  const attributeTypeRefs = useWatch({ control, name: "attributes" });

  return (
    <FormSection
      title={t("common.attributes.title")}
      action={
        <SelectItemDialog
          title={t("common.attributes.dialog.title")}
          description={t("common.attributes.dialog.description")}
          searchFieldText={t("common.attributes.dialog.search")}
          addItemsButtonText={t("common.attributes.dialog.add")}
          openDialogButtonText={t("common.attributes.open")}
          items={available}
          onAdd={(ids) => onAddAttributes(ids, attributes, attributeFields.append)}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {attributeFields.fields.map((field, index) => {
          const attribute = selected.find((x) => x.id === field.attribute.id);
          return (
            attribute && (
              <Flexbox alignItems={"center"} key={attribute.id}>
                <Box flex={1}>
                  <Token
                    variant={"secondary"}
                    {...register(`attributes.${index}`)}
                    actionable
                    actionIcon={<XCircle />}
                    actionText={t("common.attributes.remove")}
                    onAction={() => attributeFields.remove(index)}
                    dangerousAction
                  >
                    {attribute.name}
                  </Token>
                </Box>
                <Box>
                  <Controller
                    control={control}
                    name={`attributes.${index}.minCount`}
                    render={({ field: { onChange, ...rest } }) => (
                      <Counter
                        {...rest}
                        min={0}
                        onChange={(value) => {
                          const currentMaxCount = attributeTypeRefs[index]?.maxCount;
                          if (currentMaxCount && currentMaxCount < value) {
                            setValue(`attributes.${index}.maxCount`, value);
                          }
                          onChange(value);
                        }}
                      />
                    )}
                  />
                </Box>
                <Box>
                  <Checkbox
                    checked={!!attributeTypeRefs[index]?.maxCount}
                    onCheckedChange={(checked) => {
                      if (checked) {
                        if (attributeTypeRefs[index]?.minCount > 0) {
                          setValue(`attributes.${index}.maxCount`, attributeTypeRefs[index]?.minCount);
                        } else {
                          setValue(`attributes.${index}.maxCount`, 1);
                        }
                      } else {
                        setValue(`attributes.${index}.maxCount`, undefined);
                      }
                    }}
                  />
                </Box>
                <Box>
                  <Controller
                    control={control}
                    name={`attributes.${index}.maxCount`}
                    render={({ field: { value, ...rest } }) => (
                      <Counter
                        {...rest}
                        min={Math.max(attributeTypeRefs[index]?.minCount, 1)}
                        value={value ?? 0}
                        disabled={!value}
                      />
                    )}
                  />
                </Box>
              </Flexbox>
            )
          );
        })}
      </Flexbox>
    </FormSection>
  );
};
