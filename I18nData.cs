using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenopurgeManageEquipment
{
    public class I18nData
    {
        public static Dictionary<string, Dictionary<string, string>> _translations = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Swap Ranged Weapon",
                ["swap_ranged_tooltip"] = "Select another soldier to swap ranged weapons with",
                ["swap_melee_button"] = "Swap Melee Weapon",
                ["swap_melee_tooltip"] = "Select another soldier to swap melee weapons with",
                ["swap_gear_button"] = "Swap Gear",
                ["swap_gear_tooltip"] = "Select another soldier to swap gear/armor with",
                ["swap_target_info"] = "Swapping {2} between {0} and {1}",
                ["Melee"] = "Melee",
                ["Ranged"] = "Ranged",
                ["Gear"] = "Gear",
            },
            ["el"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Ανταλλαγή Όπλου Μακρινής Μάχης",
                ["swap_ranged_tooltip"] = "Επιλέξτε άλλον στρατιώτη για ανταλλαγή όπλων μακρινής μάχης",
                ["swap_melee_button"] = "Ανταλλαγή Όπλου Κοντινής Μάχης",
                ["swap_melee_tooltip"] = "Επιλέξτε άλλον στρατιώτη για ανταλλαγή όπλων κοντινής μάχης",
                ["swap_gear_button"] = "Ανταλλαγή Εξοπλισμού",
                ["swap_gear_tooltip"] = "Επιλέξτε άλλον στρατιώτη για ανταλλαγή εξοπλισμού/πανοπλίας",
                ["swap_target_info"] = "Ανταλλαγή {2} μεταξύ {0} και {1}",
                ["Melee"] = "Κοντινή Μάχη",
                ["Ranged"] = "Μακρινή Μάχη",
                ["Gear"] = "Εξοπλισμός",
            },
            ["es"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Intercambiar Arma a Distancia",
                ["swap_ranged_tooltip"] = "Selecciona otro soldado para intercambiar armas a distancia",
                ["swap_melee_button"] = "Intercambiar Arma Cuerpo a Cuerpo",
                ["swap_melee_tooltip"] = "Selecciona otro soldado para intercambiar armas cuerpo a cuerpo",
                ["swap_gear_button"] = "Intercambiar Equipo",
                ["swap_gear_tooltip"] = "Selecciona otro soldado para intercambiar equipo/armadura",
                ["swap_target_info"] = "Intercambiando {2} entre {0} y {1}",
                ["Melee"] = "Cuerpo a Cuerpo",
                ["Ranged"] = "A Distancia",
                ["Gear"] = "Equipo",
            },
            ["fr"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Échanger Arme à Distance",
                ["swap_ranged_tooltip"] = "Sélectionnez un autre soldat pour échanger des armes à distance",
                ["swap_melee_button"] = "Échanger Arme de Mêlée",
                ["swap_melee_tooltip"] = "Sélectionnez un autre soldat pour échanger des armes de mêlée",
                ["swap_gear_button"] = "Échanger Équipement",
                ["swap_gear_tooltip"] = "Sélectionnez un autre soldat pour échanger équipement/armure",
                ["swap_target_info"] = "Échange de {2} entre {0} et {1}",
                ["Melee"] = "Mêlée",
                ["Ranged"] = "À Distance",
                ["Gear"] = "Équipement",
            },
            ["de"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Fernkampfwaffe Tauschen",
                ["swap_ranged_tooltip"] = "Wählen Sie einen anderen Soldaten zum Tausch von Fernkampfwaffen",
                ["swap_melee_button"] = "Nahkampfwaffe Tauschen",
                ["swap_melee_tooltip"] = "Wählen Sie einen anderen Soldaten zum Tausch von Nahkampfwaffen",
                ["swap_gear_button"] = "Ausrüstung Tauschen",
                ["swap_gear_tooltip"] = "Wählen Sie einen anderen Soldaten zum Tausch von Ausrüstung/Rüstung",
                ["swap_target_info"] = "{2} wird zwischen {0} und {1} getauscht",
                ["Melee"] = "Nahkampf",
                ["Ranged"] = "Fernkampf",
                ["Gear"] = "Ausrüstung",
            },
            ["pl"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Wymień Broń Dystansową",
                ["swap_ranged_tooltip"] = "Wybierz innego żołnierza, aby wymienić broń dystansową",
                ["swap_melee_button"] = "Wymień Broń do Walki Wręcz",
                ["swap_melee_tooltip"] = "Wybierz innego żołnierza, aby wymienić broń do walki wręcz",
                ["swap_gear_button"] = "Wymień Wyposażenie",
                ["swap_gear_tooltip"] = "Wybierz innego żołnierza, aby wymienić wyposażenie/zbroję",
                ["swap_target_info"] = "Wymiana {2} między {0} a {1}",
                ["Melee"] = "Walka Wręcz",
                ["Ranged"] = "Dystansowa",
                ["Gear"] = "Wyposażenie",
            },
            ["pt"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Trocar Arma de Longo Alcance",
                ["swap_ranged_tooltip"] = "Selecione outro soldado para trocar armas de longo alcance",
                ["swap_melee_button"] = "Trocar Arma Corpo a Corpo",
                ["swap_melee_tooltip"] = "Selecione outro soldado para trocar armas corpo a corpo",
                ["swap_gear_button"] = "Trocar Equipamento",
                ["swap_gear_tooltip"] = "Selecione outro soldado para trocar equipamento/armadura",
                ["swap_target_info"] = "Trocando {2} entre {0} e {1}",
                ["Melee"] = "Corpo a Corpo",
                ["Ranged"] = "Longo Alcance",
                ["Gear"] = "Equipamento",
            },
            ["ru"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Обменять Дальнобойное Оружие",
                ["swap_ranged_tooltip"] = "Выберите другого солдата для обмена дальнобойным оружием",
                ["swap_melee_button"] = "Обменять Оружие Ближнего Боя",
                ["swap_melee_tooltip"] = "Выберите другого солдата для обмена оружием ближнего боя",
                ["swap_gear_button"] = "Обменять Снаряжение",
                ["swap_gear_tooltip"] = "Выберите другого солдата для обмена снаряжением/бронёй",
                ["swap_target_info"] = "Обмен {2} между {0} и {1}",
                ["Melee"] = "Ближний Бой",
                ["Ranged"] = "Дальний Бой",
                ["Gear"] = "Снаряжение",
            },
            ["uk"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "Обміняти Далекобійну Зброю",
                ["swap_ranged_tooltip"] = "Виберіть іншого солдата для обміну далекобійною зброєю",
                ["swap_melee_button"] = "Обміняти Зброю Ближнього Бою",
                ["swap_melee_tooltip"] = "Виберіть іншого солдата для обміну зброєю ближнього бою",
                ["swap_gear_button"] = "Обміняти Спорядження",
                ["swap_gear_tooltip"] = "Виберіть іншого солдата для обміну спорядженням/бронею",
                ["swap_target_info"] = "Обмін {2} між {0} та {1}",
                ["Melee"] = "Ближній Бій",
                ["Ranged"] = "Далекий Бій",
                ["Gear"] = "Спорядження",
            },
            ["ja"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "遠距離武器を交換",
                ["swap_ranged_tooltip"] = "遠距離武器を交換する別の兵士を選択",
                ["swap_melee_button"] = "近接武器を交換",
                ["swap_melee_tooltip"] = "近接武器を交換する別の兵士を選択",
                ["swap_gear_button"] = "装備を交換",
                ["swap_gear_tooltip"] = "装備/防具を交換する別の兵士を選択",
                ["swap_target_info"] = "{0} と {1} の間で {2} を交換中",
                ["Melee"] = "近接",
                ["Ranged"] = "遠距離",
                ["Gear"] = "装備",
            },
            ["ko"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "원거리 무기 교환",
                ["swap_ranged_tooltip"] = "원거리 무기를 교환할 다른 병사를 선택하세요",
                ["swap_melee_button"] = "근접 무기 교환",
                ["swap_melee_tooltip"] = "근접 무기를 교환할 다른 병사를 선택하세요",
                ["swap_gear_button"] = "장비 교환",
                ["swap_gear_tooltip"] = "장비/갑옷을 교환할 다른 병사를 선택하세요",
                ["swap_target_info"] = "{0}와(과) {1} 사이에서 {2} 교환 중",
                ["Melee"] = "근접",
                ["Ranged"] = "원거리",
                ["Gear"] = "장비",
            },
            ["zh"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "交换远程武器",
                ["swap_ranged_tooltip"] = "选择另一个士兵交换远程武器",
                ["swap_melee_button"] = "交换近战武器",
                ["swap_melee_tooltip"] = "选择另一个士兵交换近战武器",
                ["swap_gear_button"] = "交换装备",
                ["swap_gear_tooltip"] = "选择另一个士兵交换装备/护甲",
                ["swap_target_info"] = "在 {0} 和 {1} 之间交换 {2}",
                ["Melee"] = "近战",
                ["Ranged"] = "远程",
                ["Gear"] = "装备",
            },
            ["zh-TW"] = new Dictionary<string, string>
            {
                ["swap_ranged_button"] = "交換遠程武器",
                ["swap_ranged_tooltip"] = "選擇另一個士兵交換遠程武器",
                ["swap_melee_button"] = "交換近戰武器",
                ["swap_melee_tooltip"] = "選擇另一個士兵交換近戰武器",
                ["swap_gear_button"] = "交換裝備",
                ["swap_gear_tooltip"] = "選擇另一個士兵交換裝備/護甲",
                ["swap_target_info"] = "在 {0} 和 {1} 之間交換 {2}",
                ["Melee"] = "近戰",
                ["Ranged"] = "遠程",
                ["Gear"] = "裝備",
            }
        };
    }
}
