import { State } from "@mimirorg/typelibrary-types";
import { DocumentDuplicate, PencilSquare, Trash, ChevronUp } from "@styled-icons/heroicons-outline";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchAspectObjectState } from "external/sources/aspectobject/aspectObject.queries";
import { AspectObjectPreview } from "features/common/aspectobject";
import { PlainLink } from "features/common/plain-link";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useButtonStateFilter } from "features/explore/search/hooks/useButtonFilter";

export type AspectObjectSearchItemProps = AspectObjectItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user: UserItem;
};

/**
 * Component which visualizes a single aspect object search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param aspectObject
 * @param user
 * @constructor
 */
export const AspectObjectSearchItem = ({
  isSelected,
  setSelected,
  user,
  ...aspectObject
}: AspectObjectSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<AspectObjectPreview {...aspectObject} />}
    description={<ItemDescription onClick={setSelected} {...aspectObject} />}
    actions={<AspectObjectSearchItemActions user={user} aspectObject={aspectObject} />}
  />
);

type AspectObjectSearchItemActionProps = {
  user: UserItem;
  aspectObject?: AspectObjectItem;
};

const AspectObjectSearchItemActions = ({ user, aspectObject }: AspectObjectSearchItemActionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patcMutation = usePatchAspectObjectState();
  const btnFilter = useButtonStateFilter(aspectObject ?? null, user);

  if (user == null || aspectObject == null) return <></>;

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patcMutation.mutate({ id: aspectObject.id, state: State.Delete }),
  };

  const approveAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: aspectObject.id, state: State.Approve }),
  };

  const cloneLink = btnFilter.clone ? `/form/aspectObject/clone/${aspectObject.id}` : "#";
  const editLink = btnFilter.edit ? `/form/aspectObject/edit/${aspectObject.id}` : "#";

  return (
    <>
      <PlainLink tabIndex={-1} to={cloneLink}>
        <Button
          disabled={!btnFilter.clone}
          tabIndex={0}
          as={!btnFilter.clone ? "button" : "span"}
          icon={<DocumentDuplicate />}
          iconOnly
        >
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={editLink}>
        <Button
          disabled={!btnFilter.edit}
          tabIndex={0}
          as={!btnFilter.edit ? "button" : "span"}
          icon={<PencilSquare />}
          iconOnly
        >
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete")}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={
          <AspectObjectPreview
            name={aspectObject.name}
            color={aspectObject.color}
            img={aspectObject.img}
            terminals={aspectObject.terminals}
          />
        }
      >
        <Button
          disabled={!btnFilter.delete}
          variant={btnFilter.deleted ? "outlined" : "filled"}
          icon={<Trash />}
          iconOnly
        >
          {t("search.item.delete")}
        </Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveAction]}
        title={t("search.item.templates.approve")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={
          <AspectObjectPreview
            name={aspectObject.name}
            color={aspectObject.color}
            img={aspectObject.img}
            terminals={aspectObject.terminals}
          />
        }
      >
        <Button
          disabled={!btnFilter.approve}
          variant={btnFilter.approved ? "outlined" : "filled"}
          icon={<ChevronUp />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>
    </>
  );
};
