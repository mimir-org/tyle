import { ComponentMeta, ComponentStory } from "@storybook/react";
import { ItemProps } from "features/explore/search/components/item/Item";
import { Default as Item } from "features/explore/search/components/item/Item.stories";
import { ItemList } from "features/explore/search/components/item/ItemList";

const mockData = [...Array(10)].map((_, i) => <Item key={i} {...(Item.args as ItemProps)} />);

export default {
  title: "Explore/Search/Item/ItemList",
  component: ItemList,
} as ComponentMeta<typeof ItemList>;

const Template: ComponentStory<typeof ItemList> = (args) => <ItemList {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: mockData,
};
