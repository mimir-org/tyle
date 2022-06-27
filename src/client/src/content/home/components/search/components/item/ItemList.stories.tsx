import { ComponentMeta, ComponentStory } from "@storybook/react";
import { ItemProps } from "./Item";
import { Default as Item } from "./Item.stories";
import { ItemList } from "./ItemList";

const mockData = [...Array(10)].map((_, i) => <Item key={i} {...(Item.args as ItemProps)} />);

export default {
  title: "Content/Home/Search/Item/ItemList",
  component: ItemList,
} as ComponentMeta<typeof ItemList>;

const Template: ComponentStory<typeof ItemList> = (args) => <ItemList {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: mockData,
};
