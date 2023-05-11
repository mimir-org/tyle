import { SearchResult } from "./types/searchResult";
import { SelectedInfo } from "../common/selectedInfo";
import { UserItem } from "../../../common/types/userItem";
import { Item } from "./components/item/Item";
import { TerminalPreview } from "../../entities/entityPreviews/terminal/TerminalPreview";
import { TerminalItem } from "../../../common/types/terminalItem";
import { ItemDescription } from "./components/item/ItemDescription";
import { SearchItemActions } from "./components/SearchItemActions";
import { AspectObjectItem } from "../../../common/types/aspectObjectItem";
import AttributePreview from "../../entities/entityPreviews/attribute/AttributePreview";
import { toFormAttributeLib } from "../../entities/attributes/types/formAttributeLib";
import { AttributeLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import UnitPreview from "../../entities/entityPreviews/unit/UnitPreview";
import { FormUnitHelper } from "../../entities/units/types/FormUnitHelper";
import DatumPreview from "../../entities/entityPreviews/datum/DatumPreview";
import { DatumItem } from "../../../common/types/datumItem";
import { RdsItem } from "../../../common/types/rdsItem";
import { RdsPreview } from "../../entities/entityPreviews/rds/RdsPreview";
import { AspectObjectPreview } from "../../entities/entityPreviews/aspectobject/AspectObjectPreview";

interface SearchResultsRendererProps {
  item: SearchResult;
  selectedItemId?: string;
  setSelected: (item: SelectedInfo) => void;
  user: UserItem;
}

export function SearchResultsRenderer({
  item,
  selectedItemId,
  setSelected,
  user,
}: SearchResultsRendererProps): JSX.Element {
  const currentlySelected = item.id === selectedItemId;
  switch (item.kind) {
    case "TerminalItem":
      return (
        <Item
          isSelected={currentlySelected}
          preview={<TerminalPreview {...(item as TerminalItem)} />}
          onClick={() => setSelected({ id: item.id, type: "terminal" })}
          description={<ItemDescription {...(item as TerminalItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "AspectObjectItem":
      return (
        <Item
          isSelected={currentlySelected}
          preview={<AspectObjectPreview {...(item as AspectObjectItem)} />}
          onClick={() => setSelected({ id: item.id, type: "aspectObject" })}
          description={<ItemDescription {...(item as AspectObjectItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "AttributeItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "attribute" })}
          preview={<AttributePreview small {...toFormAttributeLib(item as AttributeLibCm)} />}
          description={<ItemDescription {...(item as AttributeLibCm)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "UnitItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "unit" })}
          preview={<UnitPreview small {...(item as FormUnitHelper)} />}
          description={<ItemDescription {...(item as UnitLibCm)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "DatumItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "datum" })}
          preview={<DatumPreview {...(item as DatumItem)} small />}
          description={<ItemDescription {...(item as DatumItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "RdsItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "rds" })}
          preview={<RdsPreview small {...(item as RdsItem)} />}
          description={<ItemDescription {...(item as DatumItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    default:
      return (
        <div>
          <p>Something went wrong loading this property...</p>
        </div>
      );
  }
}
