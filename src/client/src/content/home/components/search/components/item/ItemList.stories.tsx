import { ComponentMeta, ComponentStory } from "@storybook/react";
import { ItemList } from "./ItemList";
import { Default as Item } from "./Item.stories";
import { ItemProps } from "./Item";

const mockData = [...Array(10)].map((_, i) => <Item key={i} {...(Item.args as ItemProps)} />);

export default {
  title: "Content/Home/Search/Item/ItemList",
  component: ItemList,
  args: {
    children: mockData,
  },
} as ComponentMeta<typeof ItemList>;

const Template: ComponentStory<typeof ItemList> = (args) => <ItemList {...args} />;

export const Default = Template.bind({});
