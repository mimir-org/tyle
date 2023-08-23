import { BlockItem } from "../../../common/types/blockItem";
import { TerminalItem } from "../../../common/types/terminalItem";
import { AttributeItem } from "../../../common/types/attributeItem";
import { UnitItem } from "../../../common/types/unitItem";
import { QuantityDatumItem } from "../../../common/types/quantityDatumItem";
import { RdsItem } from "../../../common/types/rdsItem";

export type ItemType = BlockItem | TerminalItem | AttributeItem | UnitItem | QuantityDatumItem | RdsItem;
