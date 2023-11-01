import { SearchResult } from "./searchResult";
import { SelectedInfo } from "../../common/types/selectedInfo";
import { UserItem } from "../../common/types/userItem";
import { Item } from "./Item";
import { ItemDescription } from "./ItemDescription";
import { SearchItemActions } from "./SearchItemActions";
import { BlockPreview } from "components/BlockPreview/BlockPreview";
import { BlockItem } from "common/types/blockItem";
import { TerminalPreview } from "components/TerminalPreview/TerminalPreview";
import { TerminalItem } from "common/types/terminalItem";
import { AttributeItem } from "common/types/attributeItem";
import AttributePreview from "components/AttributePreview/AttributePreview";

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
          preview={<AttributePreview small {...(item as AttributeItem)} />}
          description={<ItemDescription {...(item as AttributeItem)} />}
          actions={<SearchItemActions user={user} item={item} />}
        />
      );
    /*case "AttributeGroupItem":
      return (
        <Item
          isSelected={currentlySelected}
          onClick={() => setSelected({ id: item.id, type: "attributeGroup" })}
          preview={<AttributeGroupPreview small {...toFormAttributeGroupLib(item as AttributeGroupLibCm)} />}
          description={<ItemDescription {...(item as AttributeGroupLibCm)} />}
          actions={<SearchItemActions user={user} item={item} isAttributeGroup={true} />}
        />
      );*/
    default:
      return (
        <div>
          <p>Something went wrong loading this property...</p>
        </div>
      );
  }
}
