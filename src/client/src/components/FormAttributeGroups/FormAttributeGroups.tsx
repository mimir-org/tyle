import { AttributeGroupLibCm, AttributeLibCm } from "@mimirorg/typelibrary-types";
//import { XCircle } from "@styled-icons/heroicons-outline";
import { Flexbox } from "@mimirorg/component-library";
import { FormSection } from "components/FormSection/FormSection";
import { SelectItemDialog } from "components/SelectItemDialog/SelectItemDialog";
import { ValueObject } from "common/types/valueObject";
import { UseFormRegisterReturn } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
//import { useGetAttributeGroups } from "external/sources/attributeGroup/attributeGroup.queries";
import { onAddAttributeGroup } from "./FormAttributeGroups.helpers";
import { AttributeGroupView } from "common/types/attributes/attributeGroupView";

export interface FormAttributeGroupsProps {
  fields: ValueObject<string>[];
  append: (item: ValueObject<string>) => void;
  remove: (index: number) => void;
  register: (index: number) => UseFormRegisterReturn;
  preprocess?: (attributeGroups?: AttributeGroupView[]) => AttributeGroupLibCm[];
  canAddAttributeGroups?: boolean;
  canRemoveAttributeGroups?: boolean;
  limitedAttributeGroups?: AttributeLibCm[];
}

/**
 * Reusable form section for adding attributeGroups
 *
 * @param fields
 * @param append
 * @param remove
 * @param register
 * @param preprocess pass a function to alter the attributeGroup data before it is shown to the user
 * @param canAddAttributeGroups controls if the add action is shown
 * @param canRemoveAttributeGroups controls if the remove action is shown
 * @param limitedAttributeGroups attributeGroups that cannot be removed, even if removing attributeGroups is allowed
 * @constructor
 */
export const FormAttributeGroups = ({ append, canAddAttributeGroups = true }: FormAttributeGroupsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  //const attributeQuery = useGetAttributeGroups();

  //Add get attributes also for each group? If above is not sufficent ie name
  //const attributeGroups = preprocess ? preprocess(attributeQuery.data) : attributeQuery.data ?? [];
  //const [available, selected] = resolveSelectedAndAvailableAttributeGroups(fields, attributeGroups);

  return (
    <FormSection
      title={t("attributeGroup.title")}
      action={
        canAddAttributeGroups && (
          <SelectItemDialog
            title={t("attributeGroup.dialog.title")}
            description={t("attributeGroup.dialog.description")}
            searchFieldText={t("attributeGroup.dialog.search")}
            addItemsButtonText={t("attributeGroup.dialog.add")}
            openDialogButtonText={t("attributeGroup.open")}
            items={[]}
            onAdd={(ids) => onAddAttributeGroup(ids, [], append)}
          />
        )
      }
    >
      <Flexbox flexWrap={"wrap"} gap={theme.mimirorg.spacing.xl}>
        {/*fields.map((field, index) => {
          const attributeGroup = selected.find((x) => x.id === field.value);
          return (
            attributeGroup && (
              <Token
                variant={"primary"}
                key={attributeGroup.id}
                {...register(index)}
                actionable={
                  canRemoveAttributeGroups && !limitedAttributeGroups.map((x) => x.id).includes(attributeGroup.id ?? "")
                }
                actionIcon={<XCircle />}
                actionText={t("attributes.remove")}
                onAction={() => remove(index)}
                dangerousAction
              >
                {attributeGroup.name}
              </Token>
            )
          );
        })*/}
      </Flexbox>
    </FormSection>
  );
};
