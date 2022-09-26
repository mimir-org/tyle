import { Trash } from "@styled-icons/heroicons-outline";
import { AnimatePresence } from "framer-motion";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { Flexbox } from "../../../../complib/layouts";
import { InfoItemButton } from "../../../common/info-item";
import { ValueObject } from "../../types/valueObject";
import { HasReferences } from "../form-references/FormReferences";
import { FormSection } from "../form-section/FormSection";
import { SelectItemDialog } from "../select-item-dialog/SelectItemDialog";
import {
  getSelectItemsFromTypeReferenceSubs,
  onAddUnits,
  showAttributeUnits,
  useTypeReferenceSubsAsUnits,
} from "./FormUnits.helpers";

export type HasUnitsAndReferences = { unitIdList?: ValueObject<string>[] } & HasReferences;

/**
 * Reusable form section for adding units to models that support them.
 * The units available in the section are defined by the selected type references on the model.
 *
 * Expects to be used in a context of useForm<T> where T has a typeReference and a unitIdList property.
 * Expects to be used in combination with a form that uses <FormReference/>.
 *
 * @constructor
 */
export const FormUnits = () => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "units" });
  const { control, register } = useFormContext<HasUnitsAndReferences>();

  const unitFields = useFieldArray({ control, name: "unitIdList" });
  const referenceSubs = useTypeReferenceSubsAsUnits(control, unitFields.replace);
  const selectItems = getSelectItemsFromTypeReferenceSubs(referenceSubs);

  return (
    <>
      <AnimatePresence>
        {showAttributeUnits(referenceSubs) && (
          <FormSection
            title={t("title")}
            action={
              <SelectItemDialog
                title={t("dialog.title")}
                description={t("dialog.description")}
                searchFieldText={t("dialog.search")}
                addItemsButtonText={t("dialog.add")}
                openDialogButtonText={t("open")}
                items={selectItems}
                onAdd={(ids) => onAddUnits(ids, unitFields)}
              />
            }
          >
            <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
              {unitFields.fields.map((field, index) => {
                const unit = selectItems.find((x) => x.id === field.value);
                return (
                  unit && (
                    <InfoItemButton
                      key={field.id}
                      {...register(`unitIdList.${index}`)}
                      {...unit}
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
        )}
      </AnimatePresence>
    </>
  );
};
