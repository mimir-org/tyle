import { SearchResult } from "./types/searchResult";
import { SelectedInfo } from "../common/selectedInfo";
import { UserItem } from "../../../common/types/userItem";
import { Item } from "./components/item/Item";
import { TerminalPreview } from "../../entities/entityPreviews/terminal/TerminalPreview";
import { TerminalItem } from "../../../common/types/terminalItem";
import { ItemDescription } from "./components/item/ItemDescription";
import { SearchItemActions } from "./components/SearchItemActions";
import { BlockItem } from "../../../common/types/blockItem";
import AttributePreview from "../../entities/entityPreviews/attribute/AttributePreview";
import { AttributeGroupLibCm, AttributeLibCm, UnitLibCm } from "@mimirorg/typelibrary-types";
import UnitPreview from "../../entities/entityPreviews/unit/UnitPreview";
import { FormUnitHelper } from "../../entities/units/types/FormUnitHelper";
import QuantityDatumPreview from "../../entities/entityPreviews/quantityDatum/QuantityDatumPreview";
import { QuantityDatumItem } from "../../../common/types/quantityDatumItem";
import { RdsItem } from "../../../common/types/rdsItem";
import { RdsPreview } from "../../entities/entityPreviews/rds/RdsPreview";
import { BlockPreview } from "../../entities/entityPreviews/block/BlockPreview";
import AttributeGroupPreview from "features/entities/entityPreviews/attributeGroup/AttributeGroupPreview";
import { toFormAttributeGroupLib } from "features/entities/attributeGroups/types/formAttributeGroupLib";

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
    case "BlockItem":
      return (
        <Item
          isSelected={currentlySelected}
          preview={<BlockPreview {...(item as BlockItem)} />}
          onClick={() => setSelected({ id: item.id, type: "block" })}
          description={<ItemDescription {...(item as BlockItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "AttributeItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "attribute" })}
          preview={<AttributePreview small {...toAttributeFormFields(item as AttributeLibCm)} />}
          description={<ItemDescription {...(item as AttributeLibCm)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "AttributeGroupItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "attributeGroup" })}
          preview={<AttributeGroupPreview small {...toFormAttributeGroupLib(item as AttributeGroupLibCm)} />}
          description={<ItemDescription {...(item as AttributeGroupLibCm)} />}
          actions={<SearchItemActions user={user} item={item} isAttributeGroup={true} />}
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
    case "QuantityDatumItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "quantityDatum" })}
          preview={<QuantityDatumPreview {...(item as QuantityDatumItem)} small />}
          description={<ItemDescription {...(item as QuantityDatumItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    case "RdsItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "rds" })}
          preview={<RdsPreview small {...(item as RdsItem)} />}
          description={<ItemDescription {...(item as QuantityDatumItem)} />}
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
